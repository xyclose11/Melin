import { instance } from "@/utils/axiosInstance.ts";

export default async function getUserDetails() {
    const response = await instance.get("/api/auth/User/UserDetails", {
        withCredentials: true,
    });
    return response.data;
}
