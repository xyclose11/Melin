import { createLazyFileRoute } from "@tanstack/react-router";
import { LibraryPage } from "@/LibraryPage.tsx";

export const Route = createLazyFileRoute("/library")({
    component: RouteComponent,
});

function RouteComponent() {
    return (
        <>
            <LibraryPage />
        </>
    );
}
