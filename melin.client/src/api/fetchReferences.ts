import { instance } from "@/utils/axiosInstance.ts";
import { Pagination } from "@/api/referencesQueryOptions.tsx";

type FromGroup = {
    groupNames: string[];
};
export const fetchReferences = async (
    pagination: Pagination,
    fromGroups: FromGroup,
) => {
    let fetchUrl = "";
    if (fromGroups.groupNames.length === 0) {
        fetchUrl += `Reference/references?pageNumber=${pagination.pageIndex === undefined ? 0 : pagination.pageIndex}&pageSize=${pagination.pageSize === undefined ? 15 : pagination.pageSize}`;
    } else if (fromGroups.groupNames.length >= 1) {
        fetchUrl += `get-references-from-multiple-groups?groupNames=${fromGroups.groupNames}`;
    }

    console.log(fetchUrl);

    return await instance.get(fetchUrl, {
        withCredentials: true,
    });
};
