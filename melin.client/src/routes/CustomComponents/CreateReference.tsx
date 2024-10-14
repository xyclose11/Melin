"use client";

import { BaseReferenceCreator } from "@/routes/CustomComponents/CreateRefComponents/BaseReferenceCreator.tsx";
import { ReferenceTypeSelector } from "@/routes/CustomComponents/CreateRefComponents/ReferenceTypeSelector.tsx";
export function CreateReference() {
    return (
        <>
            <div className={"grid grid-cols-2 gap-8"}>
                <ReferenceTypeSelector />
                <BaseReferenceCreator />
            </div>
        </>
    );
}
