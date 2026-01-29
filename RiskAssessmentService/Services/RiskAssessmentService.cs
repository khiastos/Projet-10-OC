using RiskAssessmentService.Models;
using RiskAssessmentService.Services.Interfaces;

namespace RiskAssessmentService.Services
{
    public class RiskAssessmentService : IRiskAssessmentService
    {

        private static readonly List<string> triggerTerms = new()
        {
            "hémoglobine A1C",
            "microalbumine",
            "taille",
            "poids",
            "fumeur",
            "anormal",
            "cholestérol",
            "vertige",
            "rechute",
            "réaction",
            "anticorps"
        };

        public Task<RiskAssessmentResult> RiskAssessmentCalcul(PatientRiskData patientRiskData, IEnumerable<NoteData> notesData)
        {
            // Calcule de l'âge du patient
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            int age = today.Year - patientRiskData.DateOfBirth.Year;

            if (patientRiskData.DateOfBirth.AddYears(age) > today)
            {
                age--;
            }

            // Comptage des termes déclencheurs
            int triggerTermsCount = CountTriggerTerms(notesData);

            // Détermination du niveau de risque
            RiskLevel riskLevel = DetermineRiskLevel(age, patientRiskData.Sex, triggerTermsCount);

            // Création du résultat de l'évaluation des risques
            var riskAssessmentResult = new RiskAssessmentResult
            {
                Age = age,
                Sex = patientRiskData.Sex,
                TriggerTermsCount = triggerTermsCount,
                RiskLevel = riskLevel.ToString()
            };

            return Task.FromResult(riskAssessmentResult);
        }

        private static bool IsTriggerTermPresent(string term, IEnumerable<NoteData> notes)
        {
            foreach (var note in notes)
            {
                if (!string.IsNullOrWhiteSpace(note.Note) &&
                    note.Note.Contains(term, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private static int CountTriggerTerms(IEnumerable<NoteData> notes)
        {
            return triggerTerms
                .Where(term => IsTriggerTermPresent(term, notes))
                .Count();
        }

        private static RiskLevel DetermineRiskLevel(int age, string sex, int triggerTermsCount)
        {
            // EarlyOnset
            if (age >= 30 && triggerTermsCount >= 8)
            {
                return RiskLevel.EarlyOnset;
            }
            if (age < 30 && sex == "Homme" && triggerTermsCount >= 5)
            {
                return RiskLevel.EarlyOnset;
            }
            if (age < 30 && sex == "Femme" && triggerTermsCount >= 7)
            {
                return RiskLevel.EarlyOnset;
            }

            // InDanger
            if (age < 30 && sex == "Homme" && triggerTermsCount >= 3)
            {
                return RiskLevel.InDanger;
            }
            if (age < 30 && sex == "Femme" && triggerTermsCount >= 4)
            {
                return RiskLevel.InDanger;
            }
            if (age >= 30 && (triggerTermsCount == 6 || triggerTermsCount == 7))
            {
                return RiskLevel.InDanger;
            }

            // Borderline
            if (age >= 30 && (triggerTermsCount >= 2 && triggerTermsCount <= 5))
            {
                return RiskLevel.Borderline;
            }
            if (age < 30 && (triggerTermsCount >= 3 && triggerTermsCount <= 5))
            {
                return RiskLevel.Borderline;
            }

            // None
            return RiskLevel.None;
        }
    }
}
