import { createFileRoute } from "@tanstack/react-router";
import { EditReferencePage } from "@/Reference/EditReferencePage.tsx";
import { instance } from "@/utils/axiosInstance.ts";

let referenceCache: any;

export const Route = createFileRoute("/(reference)/edit-reference/$refId")({
    loader: async ({ params }) => {
        referenceCache = await instance.get(
            `Reference/get-single-reference?refId=${params.refId}`,
            { withCredentials: true },
        );
        console.log(referenceCache.data);
    },
    component: () => {
        return <EditReferencePage reference={referenceCache} />;
    },
    notFoundComponent: () => {
        return <div>Reference Not Found</div>;
    },
    pendingComponent: () => {
        return <div>Loading...</div>;
    },
});
