import { createLazyFileRoute } from "@tanstack/react-router";
import { CreateReferencePage } from "@/CreateReferencePage.tsx";

export const Route = createLazyFileRoute("/create-reference")({
    component: CreateReferencePage,
});
