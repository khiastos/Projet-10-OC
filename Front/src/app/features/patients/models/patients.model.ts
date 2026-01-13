export interface Patient {
  id: number;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  address: string;
  phoneNumber: string;
}

// Pour création (pas d'id)
export type CreatePatientDto = Omit<Patient, 'id'>;

// Pour update (id séparé)
export type UpdatePatientDto = Partial<Omit<Patient, 'id'>>;