"use client";

import { BaseReferenceCreator } from "@/routes/CustomComponents/CreateRefComponents/BaseReferenceCreator.tsx";
import { ReferenceTypeSelector } from "@/routes/CustomComponents/CreateRefComponents/ReferenceTypeSelector.tsx";
import { useState } from "react";
export function CreateReference() {
    const [refType, setRefType] = useState("book"); // default is book

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
                <BaseReferenceCreator />
            </div>
        </>
    );
}
