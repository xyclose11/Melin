import { createLazyFileRoute } from "@tanstack/react-router";
import CreateTeam from "@/Team/CreateTeam.tsx";

export const Route = createLazyFileRoute("/(team)/create-team")({
    component: CreateTeam,
});
