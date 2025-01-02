import { createLazyFileRoute } from "@tanstack/react-router";

import { SearchBar } from "@/search/SearchBar";
import { useState } from "react";
import { debounce } from "@/utils/debounce";
export const Route = createLazyFileRoute("/(search)/search")({
    component: SearchPage,
});

function SearchPage() {
    const [query, setQuery] = useState("");

    function handleQueryChange(query) {
        setQuery(query);
        // console.log(query);
    }

    const debouncedHandleQueryChange = debounce(handleQueryChange, 3000);
    return (
        <div className="flex justify-center">
            <SearchBar handleQueryChange={debouncedHandleQueryChange} />
        </div>
    );
}
