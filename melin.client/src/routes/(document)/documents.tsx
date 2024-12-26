import { createFileRoute } from "@tanstack/react-router";
import { fetchDocuments } from "@/api/fetchDocuments.ts";

export const Route = createFileRoute("/(document)/documents")({
    loader: () => fetchDocuments(),
    component: DocumentsView,
});

function DocumentsView() {
    const documents = Route.useLoaderData();
    return (
        <div>
            <div>Teams</div>
            <ul>
                {documents.map((d) => (
                    <div>
                        <div>{d.title}</div>
                        <div>{d.lastModified}</div>
                    </div>
                ))}
            </ul>
        </div>
    );
}
