import axios from "axios";

export const instance = axios.create({
    baseURL: import.meta.env.MELIN_SERVER_ADDR,
    withCredentials: false,
    timeout: 10000
});
