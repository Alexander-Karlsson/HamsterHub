interface Props {
    score: number;
    max?: number;
}

function StarRating({ score, max = 5 }: Props) {
    return (
        <div className="star-rating">
            {Array.from({ length: max }, (_, i) => (
                <span key={i} className={i < Math.round(score) ? 'star filled' : 'star'}>
                    ★
                </span>
            ))}
            <span className="score-label">{score > 0 ? score.toFixed(1) : 'Inga betyg'}</span>
        </div>
    );
}

export default StarRating;