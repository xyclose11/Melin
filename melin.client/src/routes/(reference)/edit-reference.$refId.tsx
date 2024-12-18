import { createFileRoute } from "@tanstack/react-router";
import { EditReferencePage } from "@/Reference/EditReferencePage.tsx";
import { useQuery } from "@tanstack/react-query";
import getSingleReference from "@/api/getSingleReference.ts";

export const Route = createFileRoute("/(reference)/edit-reference/$refId")({
    component: EditReferenceRoute,
    notFoundComponent: () => {
        return <div>Reference Not Found</div>;
    },
    pendingComponent: () => {
        return <div>Loading...</div>;
    },
});

function EditReferenceRoute() {
    const { refId } = Route.useParams();

    const { isLoading, data } = useQuery({
        queryKey: ["single-reference", refId],
        queryFn: () => getSingleReference(refId),
        staleTime: 10000,
    });

    if (isLoading) {
        return <div>LOADING...</div>;
    } else {
        console.log(data);
        return (
            <div>
                <EditReferencePage reference={data} />
            </div>
        );
    }
}
