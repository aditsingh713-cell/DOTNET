import axios from 'axios';
const  API_URL ="https://localhost:7182/api/department";
export const getDepartments =() => axios.get(API_URL);
export const addDepartment = (department)=> axios.post(API_URL, department);
export const deleteDepartment =(id) => axios.delete('${API_URL}/${id}');
export const getDepartmentLogs = (departmentName) => axios.get(`${API_URL}/logs/${departmentName}`);

