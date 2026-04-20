import { useEffect, useState } from 'react';
import type { HamsterDto } from '../types/types';
import { getAllHamsters } from '../api/hamsterApi';

function HamsterList() {
    const [hamsters, setHamsters] = useState<HamsterDto[]>([]);

    useEffect(() => {
        getAllHamsters().then(setHamsters);
    }, []);

    return (
        <div>
            {hamsters.map(h => (
                <div key={h.id}>
                    <h2>{h.name}</h2>
                    <p>{h.personality} – {h.pricePerDay} kr/dag</p>
                </div>
            ))}
        </div>
    );
}

export default HamsterList;