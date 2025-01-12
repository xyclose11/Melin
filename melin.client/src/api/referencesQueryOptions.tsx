import { queryOptions } from "@tanstack/react-query";
import { fetchReferences } from "@/api/fetchReferences.ts";

export type Pagination = {
    pageSize?: number;
    pageIndex?: number;
};
export const referencesQueryOptions = (pagination: Pagination) =>
    queryOptions({
        queryKey: [
            "references",
            pagination.pageIndex === null ? 1 : pagination.pageIndex,
        ],
        queryFn: () => fetchReferences(pagination, { groupNames: [] }),
    });
