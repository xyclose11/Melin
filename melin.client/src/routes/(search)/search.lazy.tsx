import { createLazyFileRoute } from "@tanstack/react-router";

import { SearchBar } from "@/search/SearchBar";
import { useState } from "react";
import { useQuery } from "@tanstack/react-query";
import { SearchNode } from "@/search/SearchNode.tsx";
import { CSLJSON } from "@/utils/CSLJSON.ts";
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

    console.log(data);

    return (
        <div className="flex justify-center w-screen max-w-screen">
            <div className="grid">
                <SearchBar handleQueryChange={handleQueryChange} />

                <div className="max-h-96">
                    <ul className="overflow-auto">
                        {data && data.length > 0 ? (
                            data.map((d) => (
                                <li key={d.id}>
                                    <SearchNode key={d.id} data={d} />
                                </li>
                            ))
                        ) : (
                            <p>No results found.</p>
                        )}
                    </ul>
                </div>
            </div>
        </div>
    );
}
