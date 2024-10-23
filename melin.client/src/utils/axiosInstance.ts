import axios from "axios";

export const instance = axios.create({
    baseURL: "https://slider.valpo.edu/",
    withCredentials: false,
    timeout: 10000,
});
