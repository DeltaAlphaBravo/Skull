import React from 'react';
import ReactDOM from 'react-dom';
import './modal.css'

const Modal = ({ innerBody, isShowing, hide }: { innerBody: JSX.Element, isShowing: boolean, hide: (value: string) => void }) => {
  let result: string;
  return (isShowing ? ReactDOM.createPortal(
    <React.Fragment>
      <div className="modal-overlay" />
      <div className="modal-wrapper" aria-modal aria-hidden tabIndex={-1} role="dialog">
        <div className="modal">
          <div className="modal-header">
            <button type="button" className="modal-close-button" data-dismiss="modal" aria-label="Close" onClick={() => { hide(result ?? "The Nameless One") }}>
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <p>
            {innerBody}
            <input type='text' onChange={(event) => result = event.currentTarget.value} />
          </p>
        </div>
      </div>
    </React.Fragment>, document.body
  ) : null);
}

export default Modal;