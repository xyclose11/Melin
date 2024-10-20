"use client";

import { ReferenceTypeSelector } from "@/routes/CustomComponents/CreateRefComponents/ReferenceTypeSelector.tsx";
import { useState } from "react";
import { CreateReferenceBook } from "@/routes/ReferenceCreationPages/CreateReferenceBook.tsx";
export function CreateReference() {
    const [refType, setRefType] = useState("artwork"); // default is book

    function handleState(newRefType: string) {
        setRefType(newRefType);
        console.log(refType);
    }

    return (
        <>
            <div className={"grid grid-cols-2 gap-2"}>
                <ReferenceTypeSelector
                    refType={refType}
                    handleState={handleState}
                />
                {refType === "book" && <CreateReferenceBook />}
            </div>
        </>
    );
}
