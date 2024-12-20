import { createFileRoute } from "@tanstack/react-router";
import { LibraryPage } from "@/LibraryPage.tsx";
import { referencesQueryOptions } from "@/api/referencesQueryOptions.tsx";

export const Route = createFileRoute("/library")({
    loaderDeps: ({ search: { pageIndex, pageSize } }) => ({
        pageIndex,
        pageSize,
    }),
    loader: ({ context: { queryClient }, deps: { pageIndex, pageSize } }) =>
        queryClient.ensureQueryData(
            referencesQueryOptions({ pageIndex, pageSize }),
        ),
    component: LibraryRoute,
});

function LibraryRoute() {
    return (
        <>
            <LibraryPage />
        </>
    );
}
