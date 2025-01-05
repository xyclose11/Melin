import { CSLJSON } from "@/utils/CSLJSON.ts";
import { instance } from "@/utils/axiosInstance.ts";
import { mapCSLToReference } from "@/utils/mapCSLToReference.ts";

export const postSingleReference = async (data: CSLJSON) => {
    try {
        const convertedData = mapCSLToReference(data);
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
