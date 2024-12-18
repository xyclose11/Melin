import { instance } from "@/utils/axiosInstance.ts";

export default async function getSingleReference(id: string) {
    const response = await instance.get(
        `/Reference/get-single-reference?refId=${id}`,
        { withCredentials: true },
    );
    return response.data;
}
