import { useState } from 'react';

interface Props {
  onClose: () => void;
}

function NewsletterModal({ onClose }: Props) {
  const [email, setEmail] = useState('');
  const [subscribed, setSubscribed] = useState(false);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setSubscribed(true);
    setTimeout(onClose, 1500);
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal" onClick={e => e.stopPropagation()}>
        {subscribed ? (
          <p className="modal-success">Tack! Du är nu prenumerant. 🐹</p>
        ) : (
          <>
            <button className="modal-close" onClick={onClose}>✕</button>
            <p className="modal-eyebrow">Nyhetsbrev</p>
            <h3>Gå aldrig utan hamster!</h3>
            <p className="modal-body">Få uppdateringar om nya hamstrar och erbjudanden direkt till inkorgen.</p>
            <form onSubmit={handleSubmit} className="modal-form">
              <input
                type="email"
                placeholder="din@email.se"
                value={email}
                onChange={e => setEmail(e.target.value)}
                required
              />
              <button type="submit">Prenumerera</button>
            </form>
          </>
        )}
      </div>
    </div>
  );
}

export default NewsletterModal;