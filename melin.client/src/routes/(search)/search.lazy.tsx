import { createLazyFileRoute } from "@tanstack/react-router";

import { CSLJSON, SearchBar } from "@/search/SearchBar";
import { useState } from "react";
import { useQuery } from "@tanstack/react-query";
export const Route = createLazyFileRoute("/(search)/search")({
    component: SearchPage,
});

function SearchPage() {
    const [query, setQuery] = useState("");

    function handleQueryChange(query: string): void {
        setQuery(query);
    }

    // Retrieve query here to avoid having to manage state
    const { data } = useQuery<CSLJSON[]>({
        queryKey: ["userSearch", query],
        enabled: false, // Disabled here to avoid multiple queries when 'query' value changes
    });

    return (
        <div className="flex justify-center">
            <SearchBar handleQueryChange={handleQueryChange} />

            {data?.map((d) => {
                return <div>{d.title}</div>;
            })}
        </div>
    );
}
