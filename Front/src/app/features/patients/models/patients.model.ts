export interface Patient {
  id: number;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  address: string;
  phoneNumber: string;
}

// mes DTOs en back pour la création et la mise à jour des patients
export interface CreatePatientDto {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  address?: string;
  phoneNumber?: string;
}

export interface UpdatePatientDto {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  address?: string;
  phoneNumber?: string;
}
