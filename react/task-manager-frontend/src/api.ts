import { FrontendTaskDto, CreateTaskRequest, UpdateTaskRequest } from './types';

const BASE_URL = 'http://localhost:5182/frontend/tasks';

export const getTasks = async (): Promise<FrontendTaskDto[]> => {
  const res = await fetch(BASE_URL);
  if (!res.ok) throw new Error('Failed to fetch tasks');
  return res.json();
};

export const createTask = async (dto: CreateTaskRequest) => {
  const res = await fetch(BASE_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(dto),
  });
  if (!res.ok) throw new Error('Failed to create task');
};

export const updateTask = async (id: number, dto: UpdateTaskRequest) => {
  const res = await fetch(`${BASE_URL}/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(dto),
  });
  if (!res.ok) throw new Error('Failed to update task');
};