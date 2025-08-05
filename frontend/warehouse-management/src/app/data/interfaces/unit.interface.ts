export interface Unit {
  id: string;
  name: string;
  state: 'Active' | 'Archived';
}

export interface CreateUnitRequest {
  name: string;
  state: 'Active' | 'Archived';
}

export interface UpdateUnitRequest {
  name: string;
}
