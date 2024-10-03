import axios from "axios";

export const instance = axios.create({
    baseURL: "https://localhost:7120/",
})

// instance.interceptors.request.use(
//     (config) => {
//         const token = localStorage.getItem('token');
//         if (token) {
//             config.headers['Authorization'] = `Bearer ${token}`;
//         }
//         return config;
//     },
//     (error) => Promise.reject(error)
// )
