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
        setRawData([...rawData, ...newRawData]);
    };

    return (
        <>
            <ImportFile
                handleFileChange={handleFileChange}
                handleRawDataChange={handleRawDataChange}
            />

            <ImportViews rawData={rawData} formattedData={files} />
            <div>
                <h3>Uploaded References</h3>
                {files.map((f) => (
                    <div key={f.id}>
                        <h4>{f.title}</h4>
                        <p>Edition: {f.edition}</p>
                        <p>Language: {f.language}</p>
                        <p>Type: {f.type}</p>
                    </div>
                ))}
            </div>
        </>
    );
}
