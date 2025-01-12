import { createFileRoute, useLoaderData } from "@tanstack/react-router";
import { LibraryPage } from "@/LibraryPage.tsx";
import { referencesQueryOptions } from "@/api/referencesQueryOptions.tsx";
import { useCookies } from "react-cookie";
import { SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar.tsx";
import { WorkspaceToolBar } from "@/CustomComponents/WorkspaceToolBar.tsx";
import { z } from "zod";
export const Route = createFileRoute("/library")({
    validateSearch: z.object({
        pageIndex: z.number().optional(),
        pageSize: z.number().optional(),
    }),
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
    const loaderData = useLoaderData({ from: Route.id });

    const [cookies] = useCookies(["sidebar:state"]);
    const defaultOpen = cookies["sidebar:state"];

    if (loaderData?.data === undefined) {
        return <div>Loading...</div>;
    }

    return (
        <div className="justify-center">
            <SidebarProvider defaultOpen={defaultOpen}>
                <WorkspaceToolBar />
                <SidebarTrigger className=" sticky top-20" />
                <LibraryPage initialData={loaderData.data.data} />
            </SidebarProvider>
        </div>
    );
}
