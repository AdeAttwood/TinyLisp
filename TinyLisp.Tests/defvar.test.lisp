
(deftest var-with-number ()
  (defvar my-var 10)
  (assert-eq my-var 10))

(deftest var-with-eval ()
  (defvar my-var (+ 5 5))
  (assert-eq my-var 10))
