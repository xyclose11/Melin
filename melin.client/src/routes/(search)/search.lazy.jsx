import { createLazyFileRoute } from "@tanstack/react-router";

import { Cite } from "@citation-js/core";
import "@citation-js/plugin-csl";
import "@citation-js/plugin-isbn";
import { SearchBar } from "@/search/SearchBar";
import { useState } from "react";
export const Route = createLazyFileRoute("/(search)/search")({
    component: SearchPage,
});
let isbn = "3832548106";

// Cite.async(isbn)
//     .then((json) => {
//         console.log(json);
//         console.log(json.data);
//         console.log(
//             json.format("bibliography", { format: "text", template: "apa" }),
//         );
//     })
//     .catch((error) => {
//         console.log("catch");
//         console.log(error);
//     });

const now = Date.now();

const log = (string, func) => console.log(string, func);

log("now", now);
function SearchPage() {
    const [query, setQuery] = useState("");

    function handleQueryChange(query) {
        setQuery(query);
        console.log(query);
    }
    return (
        <div className="flex justify-center">
            <SearchBar handleQueryChange={handleQueryChange} />
        </div>
    );
}
