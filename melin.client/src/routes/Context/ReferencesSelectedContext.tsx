import React, { createContext, useContext, useState } from "react";

type ReferenceSelectionContextType = {
    selectedReferences: number[];
    toggleReference: (id: number) => void;
    clearSelection: () => void;
};

const ReferenceSelectionContext = createContext<
    ReferenceSelectionContextType | undefined
>(undefined);

export const ReferenceSelectionProvider: React.FC<{
    children: React.ReactNode;
}> = ({ children }) => {
    const [selectedReferences, setSelectedReferences] = useState<number[]>([]);

    const toggleReference = (id: number) => {
        setSelectedReferences((prev) =>
            prev.includes(id)
                ? prev.filter((refId) => refId !== id)
                : [...prev, id],
        );
    };

    const clearSelection = () => {
        setSelectedReferences([]);
    };

    return (
        <ReferenceSelectionContext.Provider
            value={{ selectedReferences, toggleReference, clearSelection }}
        >
            {children}
        </ReferenceSelectionContext.Provider>
    );
};

export const useReferenceSelection = () => {
    const context = useContext(ReferenceSelectionContext);
    if (!context) {
        throw new Error(
            "useReferenceSelection must be used within a ReferenceSelectionProvider",
        );
    }
    return context;
};
