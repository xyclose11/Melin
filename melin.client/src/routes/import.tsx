import { createFileRoute } from "@tanstack/react-router";
import { useState } from "react";
import { ImportFile } from "@/Import/ImportFile.tsx";
import { ImportViews } from "@/Import/ImportViews.tsx";

export const Route = createFileRoute("/import")({
    component: ImportComponent,
});

function ImportComponent() {
    const [rawData, setRawData] = useState<Set<string>>(new Set());

    const handleRawDataChange = (newRawData: string[]) => {
        setRawData((prevRawData) => new Set([...prevRawData, ...newRawData]));
    };

    return (
        <div className="sm:w-[60%] md:w-[75%] lg:w-[85%] justify-center align-middle items-center grid grid-cols-1">
            <ImportFile handleRawDataChange={handleRawDataChange} />

            <ImportViews rawData={Array.from(rawData)} />
        </div>
    );
}
