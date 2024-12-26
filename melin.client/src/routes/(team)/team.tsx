import { createFileRoute } from "@tanstack/react-router";
import { fetchTeams } from "@/api/fetchTeams.ts";

export const Route = createFileRoute("/(team)/team")({
    loader: () => fetchTeams(),
    component: TeamView,
});

function TeamView() {
    const teams = Route.useLoaderData();

    return (
        <div>
            {teams.map((t) => (
                <div key={t.id}>{t.name}</div>
            ))}
        </div>
    );
}
