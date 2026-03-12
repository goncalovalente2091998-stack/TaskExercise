export interface FrontendTaskDto {
  id: number;
  title: string;
  status: 'done' | 'pending';
  createdAt: string;
}

export interface CreateTaskRequest {
  title: string;
}

export interface UpdateTaskRequest {
  isCompleted: boolean;
}