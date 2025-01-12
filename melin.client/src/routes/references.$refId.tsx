import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/references/$refId")({
    // loader: async ({ params }) => {
    //     return getReference(params.refId);
    // },
});
