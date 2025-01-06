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

    return (
        <div className="flex justify-center h-[100%] w-screen max-w-screen">
            <div className="grid gap-4">
                <SearchBar handleQueryChange={handleQueryChange} />

                <div
                    className="
                    rounded-xl
                    border
                    shadow-md
                 snap-y max-h-[650px] scroll-smooth overflow-y-scroll mb-12"
                >
                    <ul className="min-h-[25vh] min-w-[75vh] p-1">
                        {data && data.length > 0 ? (
                            data.map((d) => (
                                <li className="snap-start" key={d.id}>
                                    <SearchNode key={d.id} data={d} />
                                </li>
                            ))
                        ) : (
                            <></>
                        )}
                    </ul>
                </div>
            </div>
        </div>
    );
}
