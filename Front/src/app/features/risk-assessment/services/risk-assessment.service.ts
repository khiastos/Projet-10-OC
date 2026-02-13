import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

export interface RiskAssessmentResult {
    patientId : string;
    age : number;
    sex : string;
    triggerTermsCount : number;
    riskLevel : 'None' | 'Borderline' | 'InDanger' | 'EarlyOnset';
}

@Injectable({
    providedIn: 'root'
})
export class RiskAssessmentService {
    private apiUrl = '/api/risk-assessment';
    
    constructor(private http: HttpClient) {}

    getRiskAssessment(patientId: string) {
        return this.http.get<RiskAssessmentResult>(`${this.apiUrl}/${patientId}`);
    }
}