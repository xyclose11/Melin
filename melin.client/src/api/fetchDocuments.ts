import { instance } from "@/utils/axiosInstance.ts";

export async function fetchDocuments() {
    try {
        const res = await instance.get("Document/retrieve-all", {
            withCredentials: true,
        });

        if (res.status === 200) {
            return res.data;
        }
    } catch (e) {
        console.error(e);
    }
}
