import { instance } from "@/utils/axiosInstance.ts";
import { Pagination } from "@/api/referencesQueryOptions.tsx";

export const fetchReferences = async (pagination: Pagination) => {
    let fetchUrl = `Reference/references?pageNumber=${pagination.pageIndex === undefined ? 0 : pagination.pageIndex}&pageSize=${pagination.pageSize === undefined ? 15 : pagination.pageSize}`;

    return await instance.get(fetchUrl, {
        withCredentials: true,
    });
};
