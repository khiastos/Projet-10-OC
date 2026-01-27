export interface Note {
  id: string;
  patientId: number;
  note: string;
}

export interface NoteCreate {
  patientId: number;
  note: string;
}