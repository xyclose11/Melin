import axios from "axios";

export const instance = axios.create({
    baseURL: "http://localhost:5000/",
    withCredentials: false,
    timeout: 1000,
    headers: {
        "Content-Type": "application/json"
    }
});
