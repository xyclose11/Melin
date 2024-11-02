import { ChoiceCard } from "@/routes/ReferenceCreationPageComponents/ChoiceCard.tsx";
import { RememberSettingCheckBox } from "@/routes/CustomComponents/RememberSettingCheckBox.tsx";
import { SuggestionsButton } from "@/routes/CustomComponents/SuggestionsButton.tsx";
import { ReferenceCreationBreadCrumb } from "@/routes/CustomComponents/ReferenceCreationBreadCrumb.tsx";

export function ReferenceCreationPage() {
    return (
        <>
            <div className="">
                <ReferenceCreationBreadCrumb />
                <h1 className="font-heading mt-12 scroll-m-20 border-b pb-2 text-5xl tracking-tight first:mt-0">
                    How do you want to{" "}
                    <span className={"font-bold underline"}>create</span>?
                </h1>
                <div className="flex mt-8">
                    <div
                        id="ChoiceGrid"
                        className="grid sm:grid-cols-2 gap-4 sm:gap-4"
                    >
                        <ChoiceCard
                            options={"Manual"}
                            linkTo={"create-reference"}
                        />
                        <ChoiceCard options={"ISBN, DOI, ISSN"} />
                        <ChoiceCard options={"BibTex, BibLaTex (.bib)"} />
                        <ChoiceCard options={"CSL-JSON, JSON"} />
                        <ChoiceCard options={"File Upload (PDF)"} />
                    </div>
                    <div className={"flex flex-col ml-16"}>
                        <div className={"text-center flex-grow"}>
                            <h1 className="font-heading mt-12 scroll-m-20 border-b pb-2 text-3xl tracking-tight first:mt-0">
                                Settings
                            </h1>
                            <div
                                className={
                                    "grid sm:grid-cols-1 gap-2 sm:gap-4 h-6"
                                }
                            >
                                <RememberSettingCheckBox
                                    text={"Remember Choice"}
                                />
                                <RememberSettingCheckBox text={"Remember"} />
                                <RememberSettingCheckBox text={"Remember"} />
                                <RememberSettingCheckBox text={"Remember"} />
                            </div>
                        </div>
                        <div className="mt-auto">
                            <SuggestionsButton />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}
