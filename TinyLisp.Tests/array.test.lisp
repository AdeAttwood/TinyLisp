(defvar global-list '(1 2 3))

(deftest index-the-list ()
  (assert-eq (get global-list 1) 2))
