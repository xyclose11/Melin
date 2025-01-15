import { instance } from "@/utils/axiosInstance.ts";
import { Pagination } from "@/api/referencesQueryOptions.tsx";

type FromGroup = {
    groupNames: string[];
};
export const fetchReferences = async (
    pagination: Pagination,
    fromGroups: FromGroup | null,
) => {
    let fetchUrl = "";
    try {
        if (fromGroups === null || fromGroups.groupNames.length === 0) {
            fetchUrl += `Reference/references?pageNumber=${pagination.pageIndex === undefined ? 0 : pagination.pageIndex}&pageSize=${pagination.pageSize === undefined ? 1000 : pagination.pageSize}`;
        } else if (fromGroups.groupNames.length >= 1) {
            fetchUrl += `get-references-from-multiple-groups?groupNames=${fromGroups.groupNames}`;
        }

        const res = await instance.get(fetchUrl, {
            withCredentials: true,
        });

        if (res.status === 200) {
            return res.data.data;
        } else {
            return {};
        }
    } catch (e) {
        return {};
    }
};
