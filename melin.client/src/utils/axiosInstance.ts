import axios from "axios";

export const instance = axios.create({
    baseURL: "https://localhost:5000/",
    withCredentials: true
})
