import { CSLJSON } from "@/utils/CSLJSON.ts";
import { instance } from "@/utils/axiosInstance.ts";
import { IReference } from "@/utils/Reference.ts";

export const postSingleReference = async (data: CSLJSON) => {
    try {
        const convertedData: IReference = {
            type: data.type,
            title: data.title ?? "",
        };

        return await instance.post(
            "Reference/create-reference",
            convertedData,
            {
                withCredentials: true,
            },
        );
    } catch (e) {
        console.error(e);
    }
};
