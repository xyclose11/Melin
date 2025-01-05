import { CSLJSON } from "@/utils/CSLJSON.ts";
import { instance } from "@/utils/axiosInstance.ts";
import { mapCSLToReference } from "@/utils/mapCSLToReference.ts";

export const postSingleReference = async (data: CSLJSON) => {
    try {
        console.log(data);
        const convertedData = mapCSLToReference(data);
        console.log(convertedData);
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
