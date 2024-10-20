"use client";

import { ReferenceTypeSelector } from "@/routes/CustomComponents/CreateRefComponents/ReferenceTypeSelector.tsx";
import { useState } from "react";
import { BaseReferenceCreator } from "@/routes/CustomComponents/CreateRefComponents/BaseReferenceCreator.tsx";
import {
    artWorkSchema,
    bookSchema,
    legalCaseSchema,
    legislationSchema,
    patentSchema,
    presentationSchema,
    primarySourceSchema,
    recordingSchema,
    reportSchema,
    softwareSchema,
    websiteSchema,
} from "@/routes/ReferenceCreationPages/BaseReferenceSchema.ts";
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
                {refType === "book" && (
                    <BaseReferenceCreator refSchema={bookSchema} />
                )}
                {refType === "artwork" && (
                    <BaseReferenceCreator refSchema={artWorkSchema} />
                )}
                {refType === "case" && (
                    <BaseReferenceCreator refSchema={legalCaseSchema} />
                )}
                {refType === "patent" && (
                    <BaseReferenceCreator refSchema={patentSchema} />
                )}
                {refType === "legislation" && (
                    <BaseReferenceCreator refSchema={legislationSchema} />
                )}
                {refType === "website" && (
                    <BaseReferenceCreator refSchema={websiteSchema} />
                )}
                {refType === "report" && (
                    <BaseReferenceCreator refSchema={reportSchema} />
                )}
                {refType === "presentation" && (
                    <BaseReferenceCreator refSchema={presentationSchema} />
                )}
                {refType === "primary-source" && (
                    <BaseReferenceCreator refSchema={primarySourceSchema} />
                )}
                {refType === "audio-recording" && (
                    <BaseReferenceCreator refSchema={recordingSchema} />
                )}
                {refType === "software" && (
                    <BaseReferenceCreator refSchema={softwareSchema} />
                )}
            </div>
        </>
    );
}
