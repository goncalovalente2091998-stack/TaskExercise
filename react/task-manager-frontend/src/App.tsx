import React, { useEffect, useState } from "react";
import { getTasks, createTask, updateTask } from "./api";
import { FrontendTaskDto } from "./types";
import 'bootstrap/dist/css/bootstrap.min.css';

const App: React.FC = () => {
  const [tasks, setTasks] = useState<FrontendTaskDto[]>([]);
  const [newTitle, setNewTitle] = useState("");
  const [error, setError] = useState("");

  const fetchTasks = async () => {
    try {
      const data = await getTasks();
      setTasks(data);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!newTitle.trim()) {
      setError("Please enter a task title");
      return;
    }

    try {
      await createTask({ title: newTitle });
      setNewTitle("");
      setError("");
      fetchTasks();
    } catch (err) {
      console.error(err);
    }
  };

  const toggleStatus = async (task: FrontendTaskDto) => {
    try {
      await updateTask(task.id, { isCompleted: task.status !== "done" });
      fetchTasks();
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div className="bg-dark min-vh-100 text-light">
      <div className="container py-5">
        <h1 className="text-center mb-4">Task Manager</h1>

        <div className="card p-4 mb-4 mx-auto" style={{ maxWidth: "500px", backgroundColor: "#fff", color: "#000" }}>
          
          <form className="d-flex flex-column" onSubmit={handleSubmit}>
            <div className="d-flex">
              <input
                type="text"
                className="form-control me-2"
                placeholder="New Task "
                value={newTitle}
                onChange={(e) => {
                  setNewTitle(e.target.value);
                  setError("");
                }}
              />
              <button type="submit" className="btn btn-primary">
                Adicionar
              </button>
            </div>

            {error && (
              <small className="text-danger mt-2">
                {error}
              </small>
            )}
          </form>

        </div>

        <div className="card p-3 mx-auto" style={{ maxWidth: "500px", backgroundColor: "#9f9f9f" }}>
          <ul className="list-group list-group-flush">
            {tasks.map((task) => (
              <li
                key={task.id}
                className="list-group-item d-flex justify-content-between align-items-center"
                style={{
                  backgroundColor: task.status === "done" ? "#28a745" : "#dc3545",
                  color: "#fff",
                  marginBottom: "0.5rem",
                  borderRadius: "5px",
                }}
              >
                <span>{task.title}</span>
                <button
                  className="btn btn-light btn-sm"
                  onClick={() => toggleStatus(task)}
                >
                  {task.status === "done" ? "Mark Pending" : "Mark Done"}
                </button>
              </li>
            ))}
          </ul>
        </div>

      </div>
    </div>
  );
};

export default App;