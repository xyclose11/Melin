import { instance } from "@/utils/axiosInstance.ts";

export const fetchTeams = async () => {
    try {
        const res = await instance.get("api/Team/owned-teams", {
            withCredentials: true,
        });

        if (res.status === 200) {
            return res.data;
        }
    } catch (e) {
        console.error(e);
    }
};
