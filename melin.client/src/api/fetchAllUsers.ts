import { instance } from "@/utils/axiosInstance.ts";

export default async function fetchAllUsers() {
    return await instance.get("api/AdminDashboard/all-users", {
        withCredentials: true,
    });
}
