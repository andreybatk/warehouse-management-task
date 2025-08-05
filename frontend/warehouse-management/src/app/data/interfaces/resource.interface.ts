export interface Resource {
  id: string;
  name: string;
  state: 'Active' | 'Archived';
}

export interface CreateResourceRequest {
  name: string;
  state: 'Active' | 'Archived';
}

export interface UpdateResourceRequest {
  name: string;
}
