import { instance } from "@/utils/axiosInstance.ts";

export const fetchDocument = async (fileName: string) => {
    try {
        const res = await instance.get(
            `/Document/retrieve?fileName=${fileName}`,
            {
                withCredentials: true,
            },
        );

        if (res.status === 200) {
            return res.data;
        }
    } catch (e) {
        console.error(e);
    }
};
