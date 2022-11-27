import React from 'react';
import ReactDOM from 'react-dom';
import './modal.css'

const StringModal = ({ innerBody, isShowing, ok, cancel }: { innerBody: JSX.Element, isShowing: boolean, ok: (value: string) => void, cancel: () => void }) => {
  let result: string;
  return (isShowing ? ReactDOM.createPortal(
    <React.Fragment>
      <div className="modal-overlay" />
      <div className="modal-wrapper" aria-modal aria-hidden tabIndex={-1} role="dialog">
        <div className="modal">
          <div className="modal-header">
            <button type="button" className="modal-close-button" data-dismiss="modal" aria-label="Close" onClick={cancel}>
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          {innerBody}
          <p>
            <input type='text' onChange={(event) => result = event.currentTarget.value} />
          </p>
          <span>
            <button onClick={() => { ok(result); result = ""; }}>OK</button>
            <button onClick={cancel}>Cancel</button>
          </span>
        </div>
      </div>
    </React.Fragment>, document.body
  ) : null);
}

export default StringModal;