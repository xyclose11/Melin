import { createFileRoute } from "@tanstack/react-router";
import { useState } from "react";
import { ImportFile, ReferenceType } from "@/Import/ImportFile.tsx";
import { ImportViews } from "@/Import/ImportViews.tsx";

export const Route = createFileRoute("/import")({
    component: ImportComponent,
});

function ImportComponent() {
    const [files, setFiles] = useState<ReferenceType[]>([]);
    const [rawData, setRawData] = useState<string[]>([]);
    const handleFileChange = (newFile: ReferenceType[]) => {
        setFiles((prev) => [...prev, ...newFile]);
    };
    const handleRawDataChange = (newRawData: string[]) => {
        setRawData((prevRawData) => [
            ...prevRawData,
            ...newRawData.filter((data) => !prevRawData.includes(data)), // Add only unique items
        ]);
    };

    return (
        <>
            <ImportFile
                handleFileChange={handleFileChange}
                handleRawDataChange={handleRawDataChange}
            />

            <ImportViews rawData={rawData} formattedData={files} />
        </>
    );
}
